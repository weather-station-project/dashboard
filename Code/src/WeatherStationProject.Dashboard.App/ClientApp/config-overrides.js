module.exports = function override(config) {
  if (!config.resolve) {
    config.resolve = {
      alias: {
        'react-bootstrap-table2-toolkit-css':
          'react-bootstrap-table2-toolkit/dist/react-bootstrap-table2-toolkit.min.css',
        'react-bootstrap-table2-toolkit': 'react-bootstrap-table2-toolkit/dist/react-bootstrap-table2-toolkit.js',
      },
    };
  } else {
    config.resolve = {
      ...config.resolve,
      alias: {
        ...config.resolve.alias,
        'react-bootstrap-table2-toolkit-css':
          'react-bootstrap-table2-toolkit/dist/react-bootstrap-table2-toolkit.min.css',
        'react-bootstrap-table2-toolkit': 'react-bootstrap-table2-toolkit/dist/react-bootstrap-table2-toolkit.js',
      },
    };
  }

  return config;
};
