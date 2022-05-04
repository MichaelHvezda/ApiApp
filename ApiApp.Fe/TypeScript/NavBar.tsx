import React, { ReactNode, ReactText, useState } from "react";
//import {
//    Navbar,
//    Nav,
//    Container,
//    NavDropdown
//} from "react-bootstrap";

import {
    CNavbar,
    CContainer,
    CNavbarBrand,
    CNavItem,
    CNavLink,
    CCollapse,
    CNavbarNav,
    CNavbarToggler,
    CButton,
    CFormInput,
    CForm
} from '@coreui/react';

//export function NavbarComponent(props: any) {
//    return (
//        <Navbar bg="light" expand="lg">
//            <Container>
//                <Navbar.Brand href="/">{props.BrandName}</Navbar.Brand>
//                <Navbar.Toggle />
//            </Container>
//        </Navbar>
//    );
//}
//export function NavLinkComponent(props: any) {
//    return (
//        <Nav.Link href={props.href}>{props.name}</Nav.Link>
//    );
//}

//export function NavComponent(props: any) {
//    return (
//        <Nav className="me-auto"></Nav>
//    );
//}

//export function NavBoostComponent(props: any) {
//    console.log("NavBoostComponent");
//    console.log(props);
//    return (
//        <Nav.Link disabled>
//            Disabled
//        </Nav.Link>
//    );
//}
export function NavCompleteComponent() {
    return (
        <CNavbar colorScheme="light" className="bg-light">
            <CContainer fluid>
                <CNavbarBrand href="/" >Test</CNavbarBrand>
            </CContainer>
        </CNavbar>
    );
}

interface NavFull {
    HomeHref: string;
    BrandName: string;
    NavList: NavFullList[];
    children?: JSX.Element[];
    src: string;
}
interface NavFullList {
    Href: string;
    HrefName: string;
}

export function NavFullComponent(props: NavFull) {
    console.log("Hello from NavFullComponent: ");
    console.log(props);
    console.log("src: ", props.src);
    console.log("props children: ", props.children);

    const Picture = (props) => {
        return (
            <div>
                <img src={props.src} />
                {props.children}
            </div>
        )
    }

    return (
        <div>
            <Picture src="https://images-wixmp-ed30a86b8c4ca887773594c2.wixmp.com/f/faa48d2d-12c2-43d1-bf23-b5e99857825b/df0qcbg-31dfc7f5-dab9-4101-85fe-1b124f319219.jpg/v1/fill/w_800,h_450,q_75,strp/dream_in_colors_by_ellysiumn_df0qcbg-fullview.jpg?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJ1cm46YXBwOjdlMGQxODg5ODIyNjQzNzNhNWYwZDQxNWVhMGQyNmUwIiwiaXNzIjoidXJuOmFwcDo3ZTBkMTg4OTgyMjY0MzczYTVmMGQ0MTVlYTBkMjZlMCIsIm9iaiI6W1t7InBhdGgiOiJcL2ZcL2ZhYTQ4ZDJkLTEyYzItNDNkMS1iZjIzLWI1ZTk5ODU3ODI1YlwvZGYwcWNiZy0zMWRmYzdmNS1kYWI5LTQxMDEtODVmZS0xYjEyNGYzMTkyMTkuanBnIiwiaGVpZ2h0IjoiPD00NTAiLCJ3aWR0aCI6Ijw9ODAwIn1dXSwiYXVkIjpbInVybjpzZXJ2aWNlOmltYWdlLndhdGVybWFyayJdLCJ3bWsiOnsicGF0aCI6Ilwvd21cL2ZhYTQ4ZDJkLTEyYzItNDNkMS1iZjIzLWI1ZTk5ODU3ODI1YlwvZWxseXNpdW1uLTQucG5nIiwib3BhY2l0eSI6OTUsInByb3BvcnRpb25zIjowLjQ1LCJncmF2aXR5IjoiY2VudGVyIn19.09GrLTfKzPVtwVCPYdghIuqx7wFUjBnE7KCty5iljpY">
                <div>
                    <p>kokok</p>
                </div>
            </Picture>
            <div>
                <p>ggg</p>
                {props.children}
            </div>
        </div>
    );


}

//export function NavFullComponent(props: NavFull) {
//    console.log("Hello from NavFullComponent:");
//    console.log(props);
//    return (
//        <CNavbar colorScheme="light" className="bg-light">
//            <CContainer fluid>
//                <CNavbarBrand href={props.HomeHref} >{props.BrandName}</CNavbarBrand>
//                {
//                    this.props.NavList && this.props.NavList.length > 0 && this.props.NavList.map((navFullList: NavFullList) => {
//                        console.log(navFullList);
//                        return (
//                            <CNavItem>
//                                <CNavLink href={navFullList.Href} >{navFullList.HrefName}</CNavLink>
//                            </CNavItem>
//                        )
//                    })}
//            </CContainer>
//        </CNavbar>
//    );
//}