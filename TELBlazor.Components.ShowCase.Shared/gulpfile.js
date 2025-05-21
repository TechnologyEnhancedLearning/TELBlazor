// gulpfile.js
// qqqq 
const gulp = require("gulp");
const fs = require('fs');
const path = require('path');

gulp.task("copy-nhsuk-css", function () {
    /*C: \dev\repos\TELBlazor\TELBlazor.Components.ShowCase.Shared\TELBlazor.Components.ShowCase.Shared.csproj*/
    const cssPath = path.join(
        __dirname,
        "..",                   // go up from TELBlazor.Components to TELBlazor
        "node_modules",
        "nhse-tel-frontend",
        "dist",
        "nhsuk.css"
    );
    // destination inside this project
    const destPath = path.join(
        __dirname,
        "wwwroot",
        "css"
    );
    console.log("Copying", cssPath, "→", destPath);
    return gulp.src(cssPath, { allowEmpty: false })
        .pipe(gulp.dest(destPath));
});


//// example in gulpfile.js --in production example
//const concat = require("gulp-concat");
//gulp.task("bundle-css", function () {
//    return gulp.src([
//        "node_modules/nhse-tel-frontend/dist/nhsuk.css",
//        "wwwroot/css/app.css"
//    ])
//        .pipe(concat("site.css"))
//        .pipe(gulp.dest("wwwroot/css"));
//});


gulp.task("default", gulp.series(
    "copy-nhsuk-css"
));