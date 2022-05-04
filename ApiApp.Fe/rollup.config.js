import typescript from '@rollup/plugin-typescript';
import resolve from '@rollup/plugin-node-resolve';
import commonjs from '@rollup/plugin-commonjs';
import { terser } from 'rollup-plugin-terser';
import external from "rollup-plugin-peer-deps-external";
import replace from '@rollup/plugin-replace';

const minify = process.env.BUILD === 'production';

export default [{
    input: [
        // list all your module files that will be imported in the page
        'TypeScript/ReactModules.tsx'
    ],
    output: {
        dir: 'wwwroot',
        format: 'esm',
        sourcemap: !minify
    },
    plugins: [
        typescript({
            tsconfig: "tsconfig.json"
        }),

        resolve({ browser: true }),
        commonjs({
            include: 'node_modules/**',
            sourceMap: false
        }),
        external({
            includeDependencies: false
        }),
        replace({
            'process.env.NODE_ENV': JSON.stringify('production')
        }),
        minify && terser({
            ecma: 6,
            compress: true,
            output: {
                beautify: !minify
            }
        })
    ]
}];