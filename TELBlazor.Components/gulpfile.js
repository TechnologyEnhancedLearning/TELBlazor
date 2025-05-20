/// Summary
// Dont want to run on publish but instead make it required for the consuming project to provide the nhsukcss so it isnt twice in the consuming project
///

// gulpfile.js
//qqqq clear up if commented out isnt needed later
const gulp = require("gulp");
//const sass = require("gulp-sass")(require("sass"));
//const concat = require("gulp-concat");

//// Compile SCSS to CSS
//gulp.task("sass", function () {
//    return gulp
//        .src("./scss/**/*.scss")  // Adjust this path if needed
//        .pipe(sass().on("error", sass.logError))
//        .pipe(concat("site.css"))
//        .pipe(gulp.dest("wwwroot/css"));
//});

gulp.task("copy-nhsuk-css", function () {
    return gulp
        .src("node_modules/nhse-tel-frontend/dist/nhsuk.css")
        .pipe(gulp.dest("wwwroot/css"));
});

// Watch for changes
//gulp.task("watch", function () {
//    gulp.watch("./scss/**/*.scss", gulp.series("sass"));
//});
//,"watch"
// Default task
gulp.task("default", gulp.series("copy-nhsuk-css"));
//gulp.task("default", gulp.series("copy-nhsuk-css", "sass"));