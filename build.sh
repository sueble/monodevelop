echo "PATH ======= $PATH"
echo "WORKSPACE ======= $WORKSPACE"

if [ "$1" = "checkout" ]; then
	cd ..
	git clone git@github.com:xamarin/release-scripts
    rake -f release-scripts/wrench/monodevelop/Rakefile checkout
else
	cd .. && mv s monodevelop && cd monodevelop
    make $1
    cd .. && mv monodevelop s && cd s
fi;
