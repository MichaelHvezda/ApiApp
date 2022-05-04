
import { registerReactControl } from 'dotvvm-jscomponent-react';

//import { RechartComponent } from "./LineChart";
import { NavFullComponent} from "./NavBar";


// DotVVM Context importer 
export default (context: any) => ({
    $controls: {

        //Graff: registerReactControl(RechartComponent, {
        //    context,
        //    onMouse() { /* default empty method */ }
        //}),
        NavFull: registerReactControl(NavFullComponent, {
            context
        }),
    }
})