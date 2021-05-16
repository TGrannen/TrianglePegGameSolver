module.exports = {
    theme: {
        extend: {
            colors: {
                blazoredorange: '#ff6600'
            }
        }
    },
    purge: {
        layers: ['components', 'utilities'],
        options: {
            safelist: ['md\\:block', 'md\\:hidden']
        }
    }
};