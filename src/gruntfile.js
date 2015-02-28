'use stric'
module.exports = function(grunt) {

  grunt.initConfig({
    pkg: grunt.file.readJSON('package.json'),

      wiredep: {
      target: {
        src: 'index.html' // point to your HTML file.
      }
    }
  });


    grunt.loadNpmTasks('grunt-wiredep');



};
