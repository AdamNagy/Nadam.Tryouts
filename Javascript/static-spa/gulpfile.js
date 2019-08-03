'use strict';
 
var gulp = require('gulp');
var concat = require('gulp-concat');
var sass = require('gulp-sass');
var htmlreplace = require('gulp-html-replace');
var fs = require('fs');

sass.compiler = require('node-sass');

/**
 * Copyes lib to dist */
gulp.task('copy-lib', function () {
	let files = ['./lib/**/*.min.*'];
	
	return gulp.src(files)
		.pipe(gulp.dest('./dist/lib/'));	  
});

/**
 * Copyes content to dist */
gulp.task('copy-content', function () {
	let files = ['./src/content/**/*'];
	
	return gulp.src(files)
		.pipe(gulp.dest('./dist/content/'));
});

/**
 * Compiles scss to css and concat*/
gulp.task('build-scss', function () {
  var cssStream = gulp.src('./src/**/*.scss')
    .pipe(sass().on('error', sass.logError));
	
  return cssStream.pipe(concat("index.css"))
  .pipe(gulp.dest("src/"))
});
 
gulp.task('sass:watch', function () {
  gulp.watch('./src/**/*.scss', ['sass']);
});

/**
 * Copies all the html files found in the template named directory and 
 * copies them all into the index.html */
gulp.task('copy-parts', function () {
	return gulp.src('./src/index.html')
		.pipe(htmlreplace({
			gallery: {
				src: gulp.src('./src/gallery.part.html'),
				tpl: '%f'
			  }
		}, { allowEmpty: true }))
		.pipe(gulp.dest('dist/'));
});

/**
 * This part can be customized.
 * This example reads a thumbnail.html file, and replaces the '%s' with the given strings in the array
 * and copies one-by-one into the index.html
 * Caruseul, gallery or similar modules can be build with this as a workaround for server side code */
gulp.task('create-thumbnails', function() {

	var thumbnail = fs.readFileSync("./src/components/thumbnail.html", "utf8");

	return gulp.src('./src/index.html')
		.pipe(htmlreplace({
			thumbnails: {
				src: ['data-main.js', 'require-src.js'],	// can be put into a global variable, environment.js, config or something
				tpl: thumbnail
			  }
		}))
		.pipe(gulp.dest('dist'));
});

/**
 * Template code for site components
 * 
 * gulp.task('add-<component name>', function() {

	var component = fs.readFileSync("./src/components/<section name>.html", "utf8");

	return gulp.src('./src/index.html')
		.pipe(htmlreplace({
			<section name>: {
				src: component,
				tpl: %s
			  }
		}))
		.pipe(gulp.dest('dist'));
  });
 */

/* Add Css concatenation and minification */
/* Add javascript concatenation and minification */

/**
 * Build proj */
// gulp.task('build', gulp.series('copy-templates', 'build-style'))
