var gulp = require('gulp'),
 	concat = require('gulp-concat'),
	 uglify = require('gulp-uglify');

gulp.task('build-min', function() {
return gulp
	.src([
		'src/core/*.js',
		'src/elements/*.js',
		'src/gallery-element/*.js'])
	.pipe(uglify())
	.pipe(concat('ndm.min.js'))
	.pipe(gulp.dest('dist/'));
});

gulp.task('build', function() {
return gulp
	.src([
		'src/core/*.js',
		'src/elements/*.js',
		'src/gallery-element/*.js'])
	.pipe(concat('ndm.js'))
	.pipe(gulp.dest('dist/'));
});