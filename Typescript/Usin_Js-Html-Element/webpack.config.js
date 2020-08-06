const path = require('path');
const webpack = require('webpack');
const nodeSass = require('node-sass');

module.exports = {
	mode: 'development',
	entry: {
		nadam: './src/index.ts'
	},
	module: {
		rules: [
			{
				test: /\.ts?$/,
				use: 'ts-loader',
				exclude: /node_modules/
			}
		]
	},
	resolve: {
		extensions: [ '.ts', '.js' ]
	},
	output: {
		filename: '[name].bundle.js',
		chunkFilename: '[name].chunk.js',
		path: path.resolve(__dirname, './dist')
	}
};
