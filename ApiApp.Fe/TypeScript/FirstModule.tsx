
import { registerReactControl } from 'dotvvm-jscomponent-react';

import { RechartComponent } from "./LineChart";
import { /*NavbarComponent, NavComponent, NavLinkComponent,*/ NavFullComponent, NavItemComponent } from "./NavBar";


// DotVVM Context importer 
export default (context: any) => ({
    $controls: {

        Graff: registerReactControl(RechartComponent, {
            context,
            onMouse() { /* default empty method */ }
        }),

        //Navbar: registerReactControl(NavbarComponent, {
        //    context,
        //}),

        //Nav: registerReactControl(NavComponent, {
        //    context,
        //}),

        //NavLink: registerReactControl(NavLinkComponent, {
        //    context,
        //}),
        NavFull: registerReactControl(NavFullComponent, {
            context
        }),
        NavItem: registerReactControl(NavItemComponent, {
            context
        })

    }
})