const mix = require('laravel-mix');

/*
 |--------------------------------------------------------------------------
 | Mix Asset Management
 |--------------------------------------------------------------------------
 |
 | Mix provides a clean, fluent API for defining some Webpack build steps
 | for your Laravel application. By default, we are compiling the Sass
 | file for the application as well as bundling up all the JS files.
 |
 */
mix.options({
    publicPath: ('./')
});
mix.js('Resources/js/app.js', 'wwwroot/js')
    .sass('Resources/sass/app.scss', 'wwwroot/css');
