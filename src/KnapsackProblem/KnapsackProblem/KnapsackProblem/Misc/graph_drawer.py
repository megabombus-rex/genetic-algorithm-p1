import matplotlib.pyplot as plt

#gen100_popsizes = [100, 200, 500, 1000, 2000]
#gen100_meanValues = [1089, 958, 919, 884, 841]
#gen100_bestValues = [994, 839, 851, 807, 802]
#gen100_worstValues = [1181, 1003, 979, 897, 876]
#gen100_runtimes = [124, 176, 299, 461, 893]

#plt.plot(gen100_popsizes, gen100_meanValues, label = "Mean")
#plt.plot(gen100_popsizes, gen100_bestValues, label = "Best")
#plt.plot(gen100_popsizes, gen100_worstValues, label = "Worst")

#plt.xlabel("Population size")
#plt.ylabel("Fitness score")
#plt.title("100 Generations GA, CC - 99%, MC - 5%")

#plt.legend()

#plt.show()

#plt.plot(gen100_popsizes, gen100_runtimes)
#plt.xlabel("Population size")
#plt.ylabel("Runtime [ms]")
#plt.title("100 Generations GA, CC - 99%, MC - 5%")

#plt.show()
#gen100_popsizes = [100, 200, 500, 1000, 2000]
#gen100_meanValues = [1089, 958, 919, 884, 841]
#gen100_bestValues = [994, 839, 851, 807, 802]
#gen100_worstValues = [1181, 1003, 979, 897, 876]
#gen100_runtimes = [124, 176, 299, 461, 893]
#
#plt.plot(gen100_popsizes, gen100_meanValues, label = "Mean")
#plt.plot(gen100_popsizes, gen100_bestValues, label = "Best")
#plt.plot(gen100_popsizes, gen100_worstValues, label = "Worst")

#SA_AlphaValues = [0.1, 0.25, 0.5, 0.9, 0.99]
#SA_i100_t01_meanValues_exp = [1186, 1150, 1090, 1007, 976]
#SA_i100_t01_bestValues_exp = [1079, 971, 937, 870, 867]
#SA_i100_t01_worstValues_exp = [1312, 1252, 1226, 1180, 1151]
#SA_i100_t01_runtimes_exp = [4, 4, 6, 47, 203]

#SA_i100_t01_meanValues_lin = [1131, 1157, 1342, 1342, 1299]
#SA_i100_t01_bestValues_lin = [1021, 1086, 1205, 1249, 1100]
#SA_i100_t01_worstValues_lin = [1262, 1243, 1439, 1483, 1440]
#SA_i100_t01_runtimes_lin = [5, 3, 1.5, 2.5, 1]

#plt.xlabel("Alpha value")
#plt.ylabel("Fitness score")
#plt.title("100 Iterations SAA, T0 = 1, T_min = 0.0001, Exponential T")

#plt.plot(SA_AlphaValues, SA_i100_t01_meanValues_exp, label = "Mean")
#plt.plot(SA_AlphaValues, SA_i100_t01_bestValues_exp, label = "Best")
#plt.plot(SA_AlphaValues, SA_i100_t01_worstValues_exp, label = "Worst")

#plt.legend()

#plt.show()

#plt.xlabel("Alpha value")
#plt.ylabel("Fitness score")
#plt.title("100 Iterations SAA, T0 = 1, T_min = 0.0001, Linear T")

#plt.plot(SA_AlphaValues, SA_i100_t01_meanValues_lin, label = "Mean")
#plt.plot(SA_AlphaValues, SA_i100_t01_bestValues_lin, label = "Best")
#plt.plot(SA_AlphaValues, SA_i100_t01_worstValues_lin, label = "Worst")

#plt.legend()
#plt.show()

# Gen = 100, runtimes
#plt.plot(gen100_popsizes, gen100_runtimes)
#plt.xlabel("Population size")
#plt.ylabel("Runtime [ms]")
#plt.title("100 Generations GA, CC - 99%, MC - 5%")
# Gen = 100, runtimes - finish

# SA runtimes, Exp/Lin
#plt.plot(SA_AlphaValues, SA_i100_t01_runtimes_exp, label = "Exponential T change")
#plt.plot(SA_AlphaValues, SA_i100_t01_runtimes_lin, label = "Linear T change")
#plt.xlabel("Alpha value")
#plt.ylabel("Runtime [ms]")
#plt.title("Exponential vs Linear T change")
#plt.legend()
#plt.show()
# SA runtimes, Exp/Lin - finish


# best instances
instances = ["An32k5", "An37k6", "An39k5", "An45k6", "An48k7", "An54k7", "An60k9"]
#best_GA = [802, 968, 839, 981, 1140, 1286, 1479]
#best_RSA = [1321, 1515, 1429, 1994, 2033, 2346, 2760]
#best_SAA = [864, 1006, 934, 1093, 1197, 1350, 1591]
#best_GrA = [1139, 1372, 1098, 1485, 1405, 1439, 2042]
#optimal_v = [784, 949, 822, 944, 1073, 1167, 1354]
#plt.plot(instances, best_GA, label = "Genetic Algorithm")
#plt.plot(instances, best_RSA, label = "Random Search Algorithm")
#plt.plot(instances, best_SAA, label = "Simulated Annealing Algorithm")
#plt.plot(instances, best_GrA, label = "Greedy Algorithm")
#plt.plot(instances, optimal_v, label = "Optimal")
##
#plt.xlabel("Instances")
#plt.ylabel("Fitness score")
#plt.title("Best fitness per instance")
##
#plt.legend()
#plt.show()
## best instances - finish


mean_GA_10k = [1461, 1619, 1573, 2065, 2169, 2483, 2860]
std_GA_10k = [48, 37, 38, 43, 45, 52, 59]
mean_GA_100k = [869, 1125, 1042, 1302, 1497, 1793, 2157]
std_GA_100k = [35, 65, 83, 105, 112, 136, 106]
mean_GA_1m = [843, 1013, 894, 1040, 1189, 1290, 1514]
std_GA_1m = [28, 25, 31, 39, 36, 44, 48]

mean_RSA_10k = [1567, 1709, 1663, 2208, 2293, 2608, 2991]
std_RSA_10k = [40, 38, 47, 43, 43, 58, 57]
mean_RSA_100k = [1495, 1642, 1599, 2127, 2201, 2529, 2899]
std_RSA_100k = [40, 35, 38, 35, 44, 42, 42]
mean_RSA_1m = [1425, 1579, 1541, 2038, 2121, 2440, 2809]
std_RSA_1m = [38, 31, 30, 49, 36, 43, 49]

mean_SAA_10k = [991, 1116, 1032, 1273, 1357, 1503, 1713]
std_SAA_10k = [65, 49, 50, 79, 57, 63, 74]
mean_SAA_100k = [1016, 1123, 1025, 1267, 1362, 1500, 1715]
std_SAA_100k = [77, 55, 61, 75, 61, 79, 71]
mean_SAA_1m = [996, 1123, 1029, 1265, 1354, 1511, 1710]
std_SAA_1m = [75, 52, 57, 86, 74, 75, 66]

plt.errorbar(instances, mean_GA_10k, std_GA_10k, label = "GA 10k", marker = '.')
plt.errorbar(instances, mean_RSA_10k, std_RSA_10k, label = "RSA 10k", marker = 'o')
plt.errorbar(instances, mean_SAA_10k, std_SAA_10k, label = "SAA 10k", marker = '^')
plt.errorbar(instances, mean_GA_100k, std_GA_100k, label = "GA 100k", marker = '.')
plt.errorbar(instances, mean_RSA_100k, std_RSA_100k, label = "RSA 100k", marker = 'o')
plt.errorbar(instances, mean_SAA_100k, std_SAA_100k, label = "SAA 100k", marker = '^')
plt.errorbar(instances, mean_GA_1m, std_GA_1m, label = "GA 1m", marker = '.')
plt.errorbar(instances, mean_RSA_1m, std_RSA_1m, label = "RSA 1m", marker = 'o')
plt.errorbar(instances, mean_SAA_1m, std_SAA_1m, label = "SAA 1m", marker = '^')

plt.xlabel("Instances")
plt.ylabel("Fitness score")
plt.title("Mean fitness per instance")
#
plt.legend()
plt.show()