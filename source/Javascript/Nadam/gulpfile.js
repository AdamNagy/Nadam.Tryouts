var gulp = require('gulp');
var concat = require('gulp-concat');
var uglify = require('gulp-uglify');
var minify = require('gulp-minify');
var pump = require('pump');

gulp.task('dev', function(cb) {
    pump([
            gulp.src(['src/Nadam.Globals.*.js',
                'src/Nadam.Extensions.*.js'
            ]),
            concat('dev-Nadam.JsLib.Index.js'),
            gulp.dest('./dest/')
        ],
        cb
    );
});

gulp.task('rel', function(cb) {
    pump([
            gulp.src(['src/Nadam.Globals.*.js',
                'src/Nadam.Extensions.*.js'
            ]),
            concat('Nadam.JsLib.Index.js'),
            uglify(),
            minify(),
            gulp.dest('./../../../dist/Javascript/')
        ],
        cb
    );
});

gulp.task('default');