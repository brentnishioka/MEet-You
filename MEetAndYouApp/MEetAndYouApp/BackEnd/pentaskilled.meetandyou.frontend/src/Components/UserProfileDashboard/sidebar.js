import './sidebar.css'


export default function Sidebar() {
    return (
        <div className="sidebar">

            <div className="sidebarItem">
                <span className="firstSidebarTitle">ABOUT ME</span>
                <img src={"INSERT IMAGE"} alt="" />
                <p>
                    Lorem ipsum dolor sit amet consectetur adipisicing elit.
                    Libero neque vel repellendus eius? Eaque architecto veniam, distinctio, impedit libero eius.
                </p>
            </div>

            <div className="sidebarItem">
                <span className="sidebarTitle">CATEGORIES</span>
                <ul className="sidebarList">
                    <li className="sidebarListItem">Life</li>
                    <li className="sidebarListItem">Music</li>
                    <li className="sidebarListItem">Style</li>
                    <li className="sidebarListItem">Sport</li>
                    <li className="sidebarListItem">Tech</li>
                    <li className="sidebarListItem">Cinema</li>
                </ul>
            </div>

            <div className="sidebarItem">
                <span className="sidebarTitle">FOLLOW US</span>
                <div className="sidebarSocial">
                    <i className="sidebarIcon fab fa-facebook-square"></i>
                    <i className="sidebarIcon fab fa-twitter-square"></i>
                    <i className="sidebarIcon fab fa-pinterest-square"></i>
                    <i className="sidebarIcon fab fa-instagram-square"></i>
                </div>
            </div>
        </div>
    )
}