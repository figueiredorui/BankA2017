'use stric'
module.exports = function(grunt) {

    grunt.loadNpmTasks('grunt-wiredep');
    grunt.loadNpmTasks('grunt-contrib-copy');
    grunt.loadNpmTasks('grunt-contrib-clean');

    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),

        wiredep: {
            target: {
                src: 'index.html' // point to your HTML file.
            }
        },
        copy: {
            build: {
                files: [
                    {cwd: '../src', src: [ 'app/**' ], dest: '../dist', expand: true},
                    {cwd: '../src', src: [ 'css/**' ], dest: '../dist', expand: true},
                    {cwd: '../src', src: [ 'img/**' ], dest: '../dist', expand: true},
                    {cwd: '../src', src: [ 'vendor/**' ], dest: '../dist', expand: true},
                    {cwd: '../src', src: [ 'index.html' ], dest: '../dist', expand: true},
                ],

            },
        },
        clean: {
            build: {
                src: [ '../dist/**' ]
            },
        },
    });

    grunt.registerTask(
  'build',
  'Compiles all of the assets and copies the files to the dist directory.',
  [ 'clean', 'copy' ]
);

};
