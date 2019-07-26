const path = require('path');
var webpack = require('webpack');

module.exports = {
  entry: {
		app: './src/index.js'
	},
  module: {
    rules: [
    //   {
    //     test: /\.tsx?$/,
    //     use: 'ts-loader',
    //     exclude: /node_modules/
	//   }
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
//   optimization: {
// 		splitChunks: {
// 			chunks: 'all'
// 		}
// 	},
	mode: 'development'
};