import React from "react";
//import {
//    //Navbar,
//    Nav,
//    //Container,
//    //NavDropdown
//} from "react-bootstrap";

import {
    CNavbar,
    CContainer,
    CNavbarBrand,
    CNavItem,
    CNavLink
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

//export function NavFullComponent() {
//    return (
//        <Nav
//            activeKey="/">
//            <Nav.Item>
//                <Nav.Link href="/">Active</Nav.Link>
//            </Nav.Item>
//            <Nav.Item>
//                <Nav.Link>Link</Nav.Link>
//            </Nav.Item>
//            <Nav.Item>
//                <Nav.Link>Link</Nav.Link>
//            </Nav.Item>
//            <Nav.Item>
//                <Nav.Link disabled>
//                    Disabled
//                </Nav.Link>
//            </Nav.Item>
//        </Nav>
//    );
//}

export function NavFullComponent(props: any) {
    return (
        <CNavbar colorScheme="light" className="bg-light">
            <CContainer fluid>
                <CNavbarBrand href={props.href} >{props.brandName}</CNavbarBrand>
            </CContainer>
        </CNavbar>
    );
}
export function NavItemComponent(props: any) {
    return (
        <CNavItem>
            <CNavLink href={props.href} active>
                {props.linkName}
            </CNavLink>
        </CNavItem>
    );

}