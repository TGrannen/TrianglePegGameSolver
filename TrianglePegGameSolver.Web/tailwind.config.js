module.exports = {
  future: {
    // removeDeprecatedGapUtilities: true,
    purgeLayersByDefault: true,
  },
  purge: ['**/*.html', '**/*.razor'],
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
