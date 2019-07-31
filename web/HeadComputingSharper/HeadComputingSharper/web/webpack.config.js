const path = require('path');
var webpack = require('webpack');

module.exports = {
	entry: {
		app: './src/index.js',
		// style: './src/home/home.style.scss'
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
			},
			{
				test: /\.scss$/,
				use: [
					"style-loader", // creates style nodes from JS strings
					"css-loader", // translates CSS into CommonJS
					"sass-loader" // compiles Sass to CSS, using Node Sass by default
				]
			},
			{
				test: /\.css$/,
				use: [
					"style-loader",
					"css-loader",
				]
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