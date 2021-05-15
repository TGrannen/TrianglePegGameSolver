# Triangle Peg Game Solver

Blazor WASM PWA to show how to solve the [Triangle Peg Game](https://www.google.com/search?q=triangle+peg+game&tbm=shop)

## Tailwind setup

Tailwind has been added to this project following the guides below

* [Part 1](https://chrissainty.com/integrating-tailwind-css-with-blazor-using-gulp-part-1/)
* [Part 2](https://chrissainty.com/integrating-tailwind-css-with-blazor-using-gulp-part-2/)

To regenerate the css file to include any changes to tailwind, run the following commands:

``` bash
npm install
gulp css:dev
```

The following command will be run to generate a production css file with any unused styles removed and tailwind css minified

``` bash
gulp css:prod
```
