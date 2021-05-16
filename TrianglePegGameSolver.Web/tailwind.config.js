module.exports = {
  future: {
    // removeDeprecatedGapUtilities: true,
    purgeLayersByDefault: true,
  },
  purge: {
    content: ['**/*.html', '**/*.razor'],

    // These options are passed through directly to PurgeCSS
    options: {
      safelist: ['md:hidden', 'md:block'],
    },
  },
  theme: {
    extend: {
      colors: {
        blazoredorange: '#ff6600',
      },
    },
  },
  variants: {},
  plugins: [],
};
