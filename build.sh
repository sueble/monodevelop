echo "PATH ======= $PATH"
echo "WORKSPACE ======= $WORKSPACE"

if [ "$1" = "checkout" ]; then
	cd ..
	git clone git@github.com:xamarin/release-scripts
	cd release-scripts/wrench
	bundle install --path ~/gems
	cd ../..
    rake -f release-scripts/wrench/monodevelop/Rakefile checkout
else
	cd .. && mv s monodevelop && cd monodevelop
    make $1
    cd .. && mv monodevelop s && cd s
fi;
