if (!window.AWSC) {
    AWSC = {};
}

/**
 * This Javascript function is for Panorama initialization in AWS Console Nav (Mezzanine).
 */
AWSC.PanoramaTrackerInit = (function() {
    "use strict";

    /**
     * Checks if browser local storage is accessible.
     */
    var isLocalStorageAccessible = function() {
        var mod = "awsc-panorama";

        try {
            window.localStorage.setItem(mod, mod);
            window.localStorage.removeItem(mod);
            return true;
        } catch (e) {
            return false;
        }
    };

    /**
     * Checks if native Performance API is available.
     */
    var isPerformanceAccessible = function() {
        try {
            return window.performance.now() !== 0;
        } catch (e) {
            return false;
        }
    };

    var ALLOWLIST_ROUTE = "/panoramaroute/allowlist",
        ALLOWLIST_ENDPOINT = "global.prod.pw.analytics.console.aws.a2z.com",
        LOG_ENDPOINT = ".prod.pr.analytics.console.aws.a2z.com",
        DEFAULT_REGION = "us-east-1",
        MAX_RETRIES = 1, // retries only 1 time to prevent generating too many 4xx and 5xx errors to console
        MODALITY = "web",
        NONPROD_DOMAINS = /alpha|beta|gamma|integ|preprod|pre-prod/i,
        PANORAMA = "panorama",
        PROD_DOMAIN = "aws.amazon.com",
        PROXY = "1",
        PROXY_ENDPOINT = "console.aws.amazon.com",
        PUBLIC = "0",
        PUBLIC_ALLOWLIST_URL = ALLOWLIST_ENDPOINT + ALLOWLIST_ROUTE,
        PROXY_ALLOWLIST_URL = PROXY_ENDPOINT + ALLOWLIST_ROUTE,
        PANORAMA_LAST_CHECKED_KEY = "awsc_panorama_lastChecked_",
        ENDPOINT_PREFERENCE_KEY = "awsc_panorama_preference_",
        allowlistBackoff = 1000, // backoff time 1 second
        allowlistUrl = undefined,
        allowlistXhrTimeout = 20000,
        endpointPreference = undefined,
        finalLogEndpoint = "", // relative routing endpoint
        hardBackoff = 1000 * 60 * 5, // backoff time of 5 minutes in case both the allowlist endpoints fail
        lastCheckedTime = undefined,
        isLocalStorageAvailable = isLocalStorageAccessible(),
        isPerformanceAvailable = isPerformanceAccessible(),
        log = function() {
            return undefined;
        },
        retryCounter = 0,
        timeoutCounter = 0;

    if (window.AWSC && window.AWSC.Clog && window.AWSC.Clog.log) {
        log = window.AWSC.Clog.log;
    }

    var currentEnv = (function detectEnvironment() {
        var url = window.location.hostname,
            env;
        switch (true) {
            case NONPROD_DOMAINS.test(url):
                env = "NonProd";
                break;
            case url.indexOf(PROD_DOMAIN) !== -1:
                env = "Prod";
                break;
            default:
                env = "NonProd";
                break;
        }
        return env;
    })();

    if (currentEnv === "NonProd" && window.disablePanorama) {
        return;
    }

    /**
     * Utility function to emit a custom event upon panorama load success or failure
     * @param isEnabled flag to emit with the custom event
     */
    var dispatchPanoramaLoadEvent = function(isEnabled) {
        try {
            var panoramaLoadEvent = document.createEvent("CustomEvent");
            panoramaLoadEvent.initCustomEvent("onPanoramaLoad", true, true, { enabled: isEnabled });
            window.dispatchEvent(panoramaLoadEvent);

            if (!isEnabled) {
                window.panorama = function() {
                    console.warn("Panorama is not enabled; events will not be emitted.");
                    return undefined;
                };
                window.panorama.enabled = false;
                window.AWSC.Clog.bufferedQueue = [];
            }
        } catch (e) {
            log("dispatchPanoramaLoadError", 1);
        }
    };

    /**
     * Gets the "content" attribute's value from meta tags with a specific name
     * @param {string} metaTagName - the "name" to look for in the document's meta tag
     * @returns the attribute value or null if none is not found
     */
    var getContentAttrFromMetaTag = function(metaTagName) {
        try {
            return document.head.querySelector("meta[name='" + metaTagName + "']").getAttribute("content");
        } catch (e) {
            return null;
        }
    };

    /**
     * Call Panorama regional allowlist API to check if console is allowlisted
     * @param region current region from Mezzanine
     * @param service current console id from Mezzanine
     * @param modality modality of tracker, eg. web, mobile, cli
     * @param trackerUrl tracker bundle url from Mezzanine for initializing tracker
     */
    var makeAllowlistRequest = function(region, service, modality, trackerUrl, requestUrl) {
        if (isLocalStorageAvailable) {
            try {
                lastCheckedTime = JSON.parse(window.localStorage.getItem(PANORAMA_LAST_CHECKED_KEY + region));
            } catch (e) {
                // set value to null if there's a parsing error so that we can continue with the allowlist check
                lastCheckedTime = null;
            }

            // wait 5 minutes as a cooldown before attempting any more requests
            if (lastCheckedTime && Date.now() - lastCheckedTime < hardBackoff) {
                dispatchPanoramaLoadEvent(false);
                return;
            } else if (lastCheckedTime && Date.now() - lastCheckedTime > hardBackoff) {
                // remove localStorage keys once 5 minutes are up
                window.localStorage.removeItem(PANORAMA_LAST_CHECKED_KEY + region);
                window.localStorage.removeItem(ENDPOINT_PREFERENCE_KEY + region);
            }
        }

        var url = "https://" + requestUrl + "?modality=" + modality + "&identifier=" + service + "&region=" + region;

        var panoramaEnabled = Boolean(window.location.href.indexOf("panoramaEnabled") !== -1);

        var xhr = new XMLHttpRequest();

        xhr.open("GET", url, true);
        xhr.onreadystatechange = function() {
            if (xhr.readyState === 4 && xhr.status === 200) {
                log("panoALSuccessful", 1);

                var panoALLatency;
                if (isPerformanceAvailable) {
                    try {
                        // eslint-disable-next-line compat/compat
                        var perf = performance.getEntriesByName(url, "resource")[0];
                        panoALLatency = Math.round(perf.responseEnd - perf.startTime);
                    } catch (e) {
                        log({
                            key: "panoALPerfError",
                            value: 1,
                        });
                        isPerformanceAvailable = 0;
                        panoALLatency = Date.now() - startTime;
                    }
                } else {
                    panoALLatency = Date.now() - startTime;
                }

                log({
                    key: "panoALSuccessLatency",
                    value: panoALLatency,
                    detail: JSON.stringify({
                        requestUrl: requestUrl,
                        isPerformanceAvailable: isPerformanceAvailable,
                    }),
                    unit: "ms",
                });

                var response = JSON.parse(xhr.response);

                if (response.isPanoramaAllowed) {
                    log("panoALTrue", 1);
                }

                if (response.isPanoramaAllowed || panoramaEnabled) {
                    var trackerConfig = {};
                    if (response.trackerConfig) {
                        try {
                            trackerConfig = JSON.parse(response.trackerConfig);
                        } catch (e) {
                            log({
                                key: "panoALParseError",
                                value: 1,
                            });
                        }
                    }

                    if (trackerConfig.trackerUrl && (trackerConfig.trackerUrlRatio || 1) > Math.random()) {
                        trackerUrl = trackerConfig.trackerUrl;
                    }

                    // Add the ability to turn off PhantomJS
                    // can be used to disable sixthsense based browsers
                    if (trackerConfig.disablePhantom && !!window.callPhantom) {
                        return;
                    }

                    initializePanoramaTracker(
                        modality,
                        region,
                        window,
                        document,
                        "script",
                        trackerUrl,
                        PANORAMA,
                        null,
                        null,
                        trackerConfig
                    );
                } else {
                    // set the flag and flush out the buffer if service is not onboarded to Panorama
                    dispatchPanoramaLoadEvent(false);
                    log("panoALFalse", 1);
                }
            } else if (xhr.readyState === 4 && (xhr.status >= 400 || xhr.status === 0)) {
                log({
                    key: "panoALFailureLatency",
                    value: Date.now() - startTime,
                    unit: "ms",
                });

                if (retryCounter < MAX_RETRIES) {
                    retryCounter++;
                    if (retryCounter === 1) {
                        log({
                            key: "anyPanoALFail",
                            value: 1,
                            detail: xhr.status,
                        });
                    }

                    xhr.abort();
                    allowlistUrl = region + "." + PROXY_ALLOWLIST_URL;

                    setTimeout(function() {
                        // eslint-disable-next-line @typescript-eslint/no-use-before-define
                        makeAllowlistRequest(region, service, modality, trackerScriptUrl, allowlistUrl);
                    }, allowlistBackoff);
                } else {
                    // clear the buffer queue once we're out of retries
                    window.AWSC.Clog.bufferedQueue = [];
                    log("panoZeroRetriesLeft", 1);

                    if (isLocalStorageAvailable) {
                        window.localStorage.setItem(PANORAMA_LAST_CHECKED_KEY + region, Date.now());
                    }

                    dispatchPanoramaLoadEvent(false);
                }
            }
            // emit how many retries it took before a 200 or if retries exceeded the limit
            if (xhr.readyState === 4 && (xhr.status === 200 || retryCounter >= MAX_RETRIES)) {
                log({
                    key: "totalPanoALRetries",
                    value: retryCounter,
                });

                if (isLocalStorageAvailable) {
                    if (allowlistUrl === PUBLIC_ALLOWLIST_URL) {
                        window.localStorage.setItem(ENDPOINT_PREFERENCE_KEY + region, "0;" + Date.now());
                    } else if (allowlistUrl === region + "." + PROXY_ALLOWLIST_URL) {
                        window.localStorage.setItem(ENDPOINT_PREFERENCE_KEY + region, "1;" + Date.now());
                    }
                }
            }
        };

        xhr.ontimeout = function() {
            timeoutCounter++;
            log("panoALTimeoutExceeded", timeoutCounter);
        };

        xhr.timeout = allowlistXhrTimeout;

        var startTime = Date.now();
        xhr.send();

        if (retryCounter === 0) {
            log("anyPanoALSent", 1);
        }
        return false;
    };

    /**
     * This function is to load Panorama/Snowplow Tracker script.
     *
     * @param m The modality of the device accessing the console
     * @param r Current region from ConsoleNavService
     * @param p The window
     * @param l The document
     * @param o "script", the tag name of script elements
     * @param w The source of the Snowplow script. Make sure you get the latest version.
     * @param i The Snowplow namespace. The Snowplow user should set this.
     * @param n The new script (to be created inside the function)
     * @param g The first script on the page (to be found inside the function)
     * @param tc Tracker Configuration that is returned from the server
     */
    var initializePanoramaTracker = function(m, r, p, l, o, w, i, n, g, tc) {
        if (!p[i]) {
            p["GlobalSnowplowNamespace"] = p["GlobalSnowplowNamespace"] || [];
            p["GlobalSnowplowNamespace"].push(i);
            p[i] = function() {
                (p[i].q = p[i].q || []).push(arguments);
            };
            p[i].loadTime = Date.now();
            p[i].enabled = true;
            p[i].q = p[i].q || [];
            p[i].trackCustomEvent = function() {
                [].unshift.call(arguments, "trackCustomEvent");
                p[i].apply(this, arguments);
            };
            n = l.createElement(o);
            g = l.getElementsByTagName(o)[0];
            n.onload = function() {
                dispatchPanoramaLoadEvent(true);
            };
            n.onerror = function() {
                dispatchPanoramaLoadEvent(false);
            };
            n.async = 1;
            n.src = w;
            g.parentNode.insertBefore(n, g);
        }

        var configuration = {
            appId: "aws-console",
            console: true,
            trackerConstants: {
                whitelist: false,
                cookieDomain: "amazon.com",
                pluginsEnabledByDefault: true,
                modality: m,
            },
        };

        // We iterate through our "passed in" tracker configuration
        // and update values to our baseline configuration - this
        // allows us to set plugin configuration dynamically
        for (var tcKey in tc) {
            if (tc.hasOwnProperty(tcKey) && !configuration[tcKey]) {
                configuration[tcKey] = tc[tcKey];
            }
        }

        if (allowlistUrl === r + "." + PROXY_ALLOWLIST_URL) {
            finalLogEndpoint = "";
        } else {
            finalLogEndpoint = "//" + r + LOG_ENDPOINT;
        }

        // Initialise panorama tracker
        window.panorama("newTracker", "cf", finalLogEndpoint, configuration);
    };

    log("panoInitLoad", 1);

    if (AWSC.NavFAC) {
        AWSC.NavFAC.loadFeatures();
        if (AWSC.NavFAC.isFeatureEnabled(PANORAMA) && ConsoleNavService && ConsoleNavService.Model) {
            // Use the ConsoleNavService object as the primary source of the region, but if unavailable, use the value from the meta tag as a fallback. As a last resort, use the default 'us-east-1' region if all other values are undefined or empty
            var metaTagRegion = getContentAttrFromMetaTag("awsc-mezz-region");
            var region = ConsoleNavService.Model.currentRegionId || metaTagRegion || DEFAULT_REGION, // global console default region
                service = ConsoleNavService.Model.currentService.id,
                modality = MODALITY,
                trackerScriptUrl = document.getElementById("awsc-panorama-bundle").getAttribute("data-url"); // get Tracker bundle URL from attribute

            try {
                var lsValue = window.localStorage.getItem(ENDPOINT_PREFERENCE_KEY + region);
                var lsSplitted = lsValue.split(";");
                endpointPreference = lsSplitted[0];
                var timestamp = parseInt(lsSplitted[1], 10);

                if (timestamp && Date.now() - timestamp < hardBackoff * 3) {
                    if (endpointPreference === PROXY) {
                        finalLogEndpoint = ""; // relative routing endpoint
                        allowlistUrl = region + "." + PROXY_ALLOWLIST_URL;
                    } else {
                        finalLogEndpoint = "//" + region + LOG_ENDPOINT;
                        allowlistUrl = PUBLIC_ALLOWLIST_URL;
                    }
                } else if (timestamp && Date.now() - timestamp > hardBackoff * 3) {
                    // time exceeded, so we can attempt hitting the public endpoint
                    localStorage.removeItem(ENDPOINT_PREFERENCE_KEY + region);
                    endpointPreference = PUBLIC;
                    allowlistUrl = PUBLIC_ALLOWLIST_URL;
                }
            } catch (e) {
                endpointPreference = undefined;
                allowlistUrl = PUBLIC_ALLOWLIST_URL;
            }

            makeAllowlistRequest(region, service, modality, trackerScriptUrl, allowlistUrl);
        } else {
            if (!AWSC.NavFAC.isFeatureEnabled(PANORAMA)) {
                log("panoFacDisabled", 1);
            } else {
                // CNS = ConsoleNavService;
                // abbreviating to avoid exceeding the 32 char limit on CLog metric name
                log("panoCNSModelMissing", 1);
            }
            dispatchPanoramaLoadEvent(false);
        }
    } else {
        log("panoFacUnavailable", 1);
        dispatchPanoramaLoadEvent(false);
    }
})();
