// gulpfile.js
// qqqq
const gulp = require("gulp");
const fs = require('fs');
const path = require('path');

function getTELFrontEndPackageVersion() {
    const packageJsonPath = path.join(__dirname, "..", "node_modules", "nhse-tel-frontend", "package.json");

    console.log("Attempting to read package.json at:", packageJsonPath);

    try {
        if (fs.existsSync(packageJsonPath)) {
            const packageJsonContent = fs.readFileSync(packageJsonPath, "utf8");
            const packageJson = JSON.parse(packageJsonContent);
            console.log("Successfully read package.json. Version:", packageJson.version);
            return packageJson.version;
        } else {
            console.warn("Package.json file does not exist at:", packageJsonPath);
            return null; // Return null if file not found
        }
    } catch (error) {
        console.error("Error reading or parsing package.json:", error);
        return null; // Return null on error
    }
}

// Task that adds a second public static string to existing file
gulp.task("add-telFrontEndVersion-to-versionInfo", function (done) {
    const versionFilePath = "TELBlazorPackageVersion/VersionInfo.cs";
    const TELFrontEndPackageVersion = getTELFrontEndPackageVersion();

    if (TELFrontEndPackageVersion === null) {
        console.error("Could not determine TELFrontEndPackageVersion. Aborting task.");
        return done(new Error("Failed to get TELFrontEndPackageVersion")); // Indicate task failure
    }

    console.log(`Adding TELFrontEndPackageVersion to CS file at: ${versionFilePath}`);

    if (fs.existsSync(versionFilePath)) {
        console.log("File exists, adding TELFrontEndPackageVersion string");

        // Read existing content
        let existingContent = fs.readFileSync(versionFilePath, "utf8");
        console.log("Existing content:", existingContent);

        // Find the closing brace of the class
        const closingBraceIndex = existingContent.lastIndexOf("}");
        const secondLastBraceIndex = existingContent.lastIndexOf("}", closingBraceIndex - 1);

        if (closingBraceIndex > 0 && secondLastBraceIndex > 0) {
            // Insert the new property before the class closing brace
            const newContent = existingContent.substring(0, secondLastBraceIndex) +
                ` public static string TELFrontEndPackageVersion = "${TELFrontEndPackageVersion}"; ` +
                existingContent.substring(secondLastBraceIndex);

            fs.writeFileSync(versionFilePath, newContent);
            console.log("Second string added successfully!");
        } else {
            console.error("Couldn't find proper location to insert second string");
        }
    }

    done();
});


gulp.task("default", gulp.series("add-telFrontEndVersion-to-versionInfo"));