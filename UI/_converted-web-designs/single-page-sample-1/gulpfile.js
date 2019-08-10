var gulp = require('gulp');
var concat = require('gulp-concat');
var sass = require('gulp-sass');
var cleanCSS = require('gulp-clean-css');

gulp.task('build', function () {
	return gulp.src('./scss/**/*.scss')
	  .pipe(sass().on('error', sass.logError))
	  .pipe(cleanCSS({compatibility: 'ie8'}))
	  .pipe(concat('index.css'))
	  .pipe(gulp.dest('./'));
  });

gulp.task('concat-css-lib', function(){
	return gulp.src('./css-lib/**/*.css')
		.pipe(cleanCSS({compatibility: 'ie8'}))
		.pipe(concat('css-lib.min.css'))
		.pipe(gulp.dest('./'));
})