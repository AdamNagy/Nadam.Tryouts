const path = require('path');
var webpack = require('webpack');

module.exports = {
	mode: 'development',
	entry: './src/index.js',
	module: {
		rules: [
			{
			    test: /\.m?js$/,
			    exclude: /(node_modules|bower_components)/,
			    use: {
				    loader: 'babel-loader',
				    options: {
				    presets: ['@babel/preset-env']
				    }
			    }
			},
			{
				test: /\.css$/,
				use: ['style-loader', 'css-loader']
			}
		]
	},
	resolve: {
		extensions: [ '.tsx', '.ts', '.js' ]
	},
	output: {
		filename: 'index.bundle.js',
		path: path.resolve(__dirname, 'dist')
	},
	plugins: [
		new webpack.ProvidePlugin({
			$: 'jquery',
			jQuery: 'jquery',
			'window.jQuery': 'jquery',
			Masonry: 'masonry-layout',
			jQueryBridget: 'jquery-bridget'
		})
	],
};
