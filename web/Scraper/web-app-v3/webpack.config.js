const path = require('path');
var webpack = require('webpack');
var nodeSass = require('node-sass');

module.exports = {
	mode: 'development',
	entry: {
		app: './src/index.ts',
		bootstrap: './src/bootstrap.js'
	},
	module: {
		rules: [
			{
				test: /\.tsx?$/,
				use: 'ts-loader',
				exclude: /node_modules/
			},
			{
				test:/\.scss$/,
				use: ['style-loader', 'css-loader', 'sass-loader']
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
		filename: '[name].bundle.js',
		chunkFilename: '[name].chunk.js',
		path: path.resolve(__dirname, '../web-app-v3_dist')
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
