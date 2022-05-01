import { registerReactControl } from 'dotvvm-jscomponent-react';
import { RechartComponent } from "./NextModule";
// DotVVM Context importer 
export default (context) => ({
    $controls: {
        recharts: registerReactControl(RechartComponent, {
            context,
            onMouse() { }
        })
    }
});
//# sourceMappingURL=FirstModule.js.map