const { createProxyMiddleware } = require('http-proxy-middleware');

const context = [
    "/weatherforecast",
    "/Login",
];

module.exports = function (app) {
    const appProxy = createProxyMiddleware(context, {
        target: 'https://meetandyou.me:8001',
        secure: false
    });

    app.use(appProxy);
};
