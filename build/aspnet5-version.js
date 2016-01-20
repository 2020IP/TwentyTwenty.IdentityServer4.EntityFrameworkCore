var glob = require("glob")
var jsonfile = require('jsonfile');
var semver = require('semver');
var exec = require('child_process').exec;

var baseDir = process.env.APPVEYOR_BUILD_FOLDER;
var buildVersion = process.env.APPVEYOR_BUILD_VERSION;
var buildNumber = process.env.APPVEYOR_BUILD_NUMBER;
var semversion = semver.valid(buildVersion)

console.info("semversion: " + semversion);

glob(baseDir + "/**/project.json", null, function(er, files) {
    files.forEach(function(file) {
        console.info("Patching: " + file);
        jsonfile.readFile(file, function (readError, project) {
            project.version = semversion;
            jsonfile.writeFile(file, project, {spaces: 2}, function(writeError) {
                if(writeError) {
                    console.error("Error writing project.json: " + writeError);
                }
            });
        });
    });
});