var gulp = require("gulp");
var sass = require("gulp-sass");
var cleanCss = require('gulp-cleancss');
var rename = require("gulp-rename");
gulp.task("styles", function () {
    gulp.src("wwwroot/scss/**/*.scss")
    .pipe(sass().on("error", sass.logError))
    .pipe(gulp.dest("wwwroot/css/"))
});
gulp.task("minimize", function () {
    gulp.src("wwwroot/css/exercises.css")
    .pipe(cleanCss({ keepBreaks: false }))
    .pipe(rename("exercises.min.css"))
    .pipe(gulp.dest("wwwroot/css/min/"));
});
//Watch task
gulp.task("default", ["styles", "minimize"], function () {
    gulp.watch("wwwroot/scss/**/*.scss");
});