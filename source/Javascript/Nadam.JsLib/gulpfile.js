var gulp = require('gulp');
var concat = require('gulp-concat');
var uglify = require('gulp-uglify');
var minify = require('gulp-minify');
var pump = require('pump');

gulp.task('dev', function(cb) {
    pump([
            gulp.src(['src/*.js', ]),
            concat('Nadam.JsLib.Index.js'),
            gulp.dest('./dest/')
        ],
        cb
    );
});

gulp.task('rel', function(cb) {
    pump([
            gulp.src(['src/*.js', ]),
            concat('Nadam.JsLib.Index.js'),
            minify(),
            gulp.dest('./../../../dist/Javascript/')
        ],
        cb
    );
});

gulp.task('default');