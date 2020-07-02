var gulp = require('gulp'),
 	concat = require('gulp-concat'),
	 uglify = require('gulp-uglify');

gulp.task('build-min', function() {
return gulp
	.src([
		'src/utils.js',
		'src/array.extensions.js',
		'src/html-element.prototype.js',
		'src/html-element.selectors.js',
		'src/value-binding.js'])
	.pipe(uglify())
	.pipe(concat('ndm-core.min.js'))
	.pipe(gulp.dest('dist/'));
});

gulp.task('build', function() {
return gulp
	.src([
		'src/utils.js',
		'src/array.extensions.js',
		'src/html-element.prototype.js',
		'src/html-element.selectors.js',
		'src/value-binding.js'])
	.pipe(concat('ndm-core.js'))
	.pipe(gulp.dest('dist/'));
});