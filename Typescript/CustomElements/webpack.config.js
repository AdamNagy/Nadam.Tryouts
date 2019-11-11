const path = require('path');
const webpack = require('webpack');
const nodeSass = require('node-sass');

module.exports = {
	mode: 'development',
	entry: {
		nadam: './src/index.ts',
		bootstrap: './src/bootstrap.ts'
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
			},
			{
				test: require.resolve("jquery"),
				use: [
				  {
					loader: "expose-loader",
					options: "$"
				  }
				]
			},
			{
				test: /\.(woff(2)?|ttf|eot|svg)(\?v=\d+\.\d+\.\d+)?$/,
				use: [
				  {
					loader: 'file-loader',
					options: {
					  name: '[name].[ext]',
					  outputPath: 'fonts/'
					}
				  }
				]
			},
			{
				test: /\.(gif|png|jpe?g|svg)$/i,
				use: [
				  'file-loader',
				  {
					loader: 'image-webpack-loader',
					options: {
					  bypassOnDebug: true, // webpack@1.x
					  disable: true, // webpack@2.x and newer
					},
				  },
				],
			}
		]
	},
	resolve: {
		extensions: [ '.tsx', '.ts', '.js' ]
	},
	// output: {
	// 	filename: '[name].bundle.js',
	// 	chunkFilename: '[name].chunk.js',
	// 	path: path.resolve(__dirname, './dist')
	//   },
	plugins: [
		new webpack.ProvidePlugin({
			$: 'jquery',
			jQuery: 'jquery',
			"window.jQuery": "jquery"
		})
	],
};
