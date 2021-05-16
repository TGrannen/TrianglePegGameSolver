# Triangle Peg Game Solver

Blazor WASM PWA to show how to solve the [Triangle Peg Game](https://www.google.com/search?q=triangle+peg+game&tbm=shop)

## Tailwind setup

[Tailwind](https://tailwindcss.com/docs) has been added to this project following the guides below

* [Part 1](https://chrissainty.com/integrating-tailwind-css-with-blazor-using-gulp-part-1/)
* [Part 2](https://chrissainty.com/integrating-tailwind-css-with-blazor-using-gulp-part-2/)
* [Blazor and TailwindCSS](https://codejuration.com/blog/2020/11/blazor-tailwind/)

To regenerate the css file to include any changes to tailwind, run the following commands:

``` bash
npm install
npm run build-css-dev
```

The following command will be run to generate a production css file with any unused styles removed and tailwind css minified

``` bash
npm run build-css-prod
```

## Iconography

[HeroIcons](https://heroicons.com/) are used for Iconography. They can be used as Razor components provided by this package: [HeroIcons.Blazor](https://github.com/duaneedwards/heroicons/tree/master/blazor#readme)

## State Management

Advanced state management will us the Flux pattern with the [Fluxor](https://github.com/mrpmorris/Fluxor) package. This [tutorial](https://dev.to/mr_eking/advanced-blazor-state-management-using-fluxor-part-1-696) is helpful for learning how this works.
