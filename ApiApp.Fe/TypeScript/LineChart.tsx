import * as React from 'react';
import { LineChart, XAxis, Tooltip, CartesianGrid, Line } from 'recharts';

// react component
export function RechartComponent(props: any) {
    return (
        <LineChart
            width={400}
            height={400}
            data={props.data}
            margin={{ top: 5, right: 20, left: 10, bottom: 5 }} >
            <XAxis dataKey="name" />
            <Tooltip />
            <CartesianGrid stroke={props.stroke} />
            {
                Object.keys(props.data[0]).slice(1).map((s, i) =>
                    <Line type="monotone"
                        dataKey={s}
                        stroke={"#" + (i * 4).toString() + "87908"}
                        yAxisId={i}
                        onMouseEnter={(_) => props.onMouse(s)} />)
            }
        </LineChart>
    );
}