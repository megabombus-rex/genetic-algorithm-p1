import matplotlib.pyplot as plt

gen100_popsizes = [100, 200, 500, 1000, 2000]
gen100_meanValues = [1089, 958, 919, 884, 841]
gen100_bestValues = [994, 839, 851, 807, 802]
gen100_worstValues = [1181, 1003, 979, 897, 876]
gen100_runtimes = [124, 176, 299, 461, 893]

plt.plot(gen100_popsizes, gen100_meanValues, label = "Mean")
plt.plot(gen100_popsizes, gen100_bestValues, label = "Best")
plt.plot(gen100_popsizes, gen100_worstValues, label = "Worst")

plt.xlabel("Population size")
plt.ylabel("Fitness score")
plt.title("100 Generations GA, CC - 99%, MC - 5%")

plt.legend()

plt.show()

plt.plot(gen100_popsizes, gen100_runtimes)
plt.xlabel("Population size")
plt.ylabel("Runtime [ms]")
plt.title("100 Generations GA, CC - 99%, MC - 5%")

plt.show()