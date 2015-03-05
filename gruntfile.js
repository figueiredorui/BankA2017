'use stric'
module.exports = function(grunt) {

    grunt.loadNpmTasks('grunt-wiredep');
    grunt.loadNpmTasks('grunt-contrib-copy');
    grunt.loadNpmTasks('grunt-contrib-clean');
    grunt.loadNpmTasks('grunt-string-replace');

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
                    {cwd: 'src', src: [ 'app/**' ], dest: 'dist', expand: true},
                    {cwd: 'src', src: [ 'css/**' ], dest: 'dist', expand: true},
                    {cwd: 'src', src: [ 'img/**' ], dest: 'dist', expand: true},
                    {cwd: 'src', src: [ 'vendor/**' ], dest: 'dist', expand: true},
                    {cwd: 'src', src: [ 'index.html' ], dest: 'dist', expand: true},
                ],

            },
        },
        clean: {
            build: {
                src: [ 'dist/*']
            },
            afterbuild:{
                src: [ 'dist/vendor/jqueryui/themes/**', 
					   'dist/vendor/jqueryui/ui/**', 
					   'dist/vendor/angular-i18n/*', 
					   '!dist/vendor/angular-i18n/angular-locale_en-gb.js']
            }
        },
        'string-replace': {
            build: {
                files: {
                    'dist': 'app/common/config.js',
                },
                options: {
                    replacements: [
                        // place files inline example
                        {
                            pattern: 'http://localhost/banka.api/',
                            replacement: 'https://apibanka.apphb.com/'
                        }
                    ]
                }
            }
        }
    });

    grunt.registerTask(
        'build',
        'Compiles all of the assets and copies the files to the dist directory.',
        [ 'clean:build', 'copy','string-replace', 'clean:afterbuild' ]
    );

};
