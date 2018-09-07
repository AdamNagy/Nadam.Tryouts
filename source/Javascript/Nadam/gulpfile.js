var gulp = require('gulp');
var concat = require('gulp-concat');
var rename = require('gulp-rename');
var uglify = require('gulp-uglify');

gulp.task('concat-scripts', function(){
	return gulp.src(['src/Nadam.Globals.*.js',
					 'src/Nadam.Extensions.*.js'])
		.pipe(concat('Nadam.JsLib.Index.js'))
		.pipe(gulp.dest('dest/'));
  });

  gulp.task('default');