var glob = require("glob")
var jsonfile = require('jsonfile');
var semver = require('semver');
var exec = require('child_process').exec;

var baseDir = process.env.APPVEYOR_BUILD_FOLDER;
var buildVersion = process.env.APPVEYOR_BUILD_VERSION;
var isRelease = process.env.APPVEYOR_REPO_BRANCH  == 'master';
var buildNumber = process.env.APPVEYOR_BUILD_NUMBER;
var semversion = isRelease ? semver.valid(buildVersion) : semver.valid(buildVersion + '-pre-' + buildNumber)

// Update current build version
var cmd = "powershell -command \"& {&'Update-AppveyorBuild' -Version '" + semversion + "'}\"";
exec(cmd, function(error, stdout, stderr) {
    if(error) {
        console.error("Error updating build: " + error);
    }
});

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

if (isRelease) {
    var nextVersion = semver.inc(buildVersion, 'patch');

    // Modifiy appveyor.yml
    var pscmd = "powershell -command \"& {(Get-Content appveyor.yml) -replace 'version: \\d+.\\d+.\\d+', 'version: " + nextVersion + "' | Out-File appveyor.yml}";
    console.info("Incrementing app version.");
    console.info("Modify command: " + pscmd);
    exec(pscmd, function(error, stdout, stderr) {
        if(error) {
            console.error("Error modifying appveyor.yml: " + error);
        }
    });
}