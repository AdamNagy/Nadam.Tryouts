'use strict';
 
var gulp = require('gulp');
var htmlreplace = require('gulp-html-replace');

gulp.task('copy-templates', function () {
	return gulp.src('./src/ndm.template.html')
		.pipe(htmlreplace({
			templates: {
				src: gulp.src('./src/**/*.template.html'),
				tpl: '%s'
			  }
		}, { allowEmpty: true }))
		.pipe(gulp.dest('./dist'));
});
