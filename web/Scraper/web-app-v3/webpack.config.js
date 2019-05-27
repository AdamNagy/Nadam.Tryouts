const path = require('path');
var webpack = require('webpack');

module.exports = {
	mode: 'development',
	entry: './src/index.ts',
	module: {
		rules: [
			{
				test: /\.tsx?$/,
				use: 'ts-loader',
				exclude: /node_modules/
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
	// plugins: [
	// 	new webpack.ProvidePlugin({
	// 		$: 'jquery',
	// 		jQuery: 'jquery',
	// 		'window.jQuery': 'jquery',
	// 		Masonry: 'masonry-layout',
	// 		jQueryBridget: 'jquery-bridget'
	// 	})
	// ],
};
