// gulpfile.js
const gulp = require("gulp");
const fs = require('fs');
const path = require('path');

gulp.task("copy-nhsuk-css", function () {
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

gulp.task("default", gulp.series(
    "copy-nhsuk-css"
));