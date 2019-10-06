const path = require('path');
var webpack = require('webpack');

module.exports = {
	entry: {
		app: './src/index.js'
	},
	module: {
		rules: [
			{
				test: /\.ts(x?)$/,
				exclude: /node_modules/,
				use: 'babel-loader'
			},
			{
			test: /\.js$/,
			exclude: /\/node_modules\//,
			loader: 'babel-loader'
			}
		]
  	},
	resolve: {
		extensions: [ '.tsx', '.ts', '.js' ]
	},
	output: {
		filename: '[name].bundle.js',
		path: path.resolve(__dirname, 'dist')
	},
	plugins: [
		new webpack.ProvidePlugin({
			m: 'mithril'
		})
	],
	//   optimization: {
	// 		splitChunks: {
	// 			chunks: 'all'
	// 		}
	// 	},
	mode: 'development'
};