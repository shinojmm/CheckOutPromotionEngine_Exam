# CheckOutPromotionEngine


# Problem Statement 1: Promotion Engine
We need you to implement a simple promotion engine for a checkout process. Our Cart contains a list of single character SKU ids (A, B, C.	) over which the promotion engine will need to run.
The promotion engine will need to calculate the total order value after applying the 2 promotion types
•	buy 'n' items of a SKU for a fixed price (3 A's for 130)
•	buy SKU 1 & SKU 2 for a fixed price ( C + D = 30 )
The promotion engine should be modular to allow for more promotion types to be added at a later date (e.g. a future promotion could be x% of a SKU unit price). For this coding exercise you can assume that the promotions will be mutually exclusive; in other words if one is applied the other promotions will not apply
Test Setup

Unit price for SKU IDs 

A	50

B	30

C	20

D	15

# Active Promotions
3 of A's for 130

2 of B's for 45 

C & D for 30

# Scenario A

1	* A	50

1	* B	30

1	* C	20

Total	100

# Scenario	B	

5 * A		130 + 2*50

5 * B		45 + 45 + 30

1 * C		20

Total	370

# Scenario C

3	* A	130

5	* B	45 + 45 + 1 * 30

1	* C	-

1	* D	30

---------------------------------------------------------------------------------------------------------------------------------------------------------------------
# Solution :-

Take the latest from the below link - https://github.com/shinojmm/CheckOutPromotionEngine_Exam.git

# Tools needed-

# Visual studio - 2019

# Dot net version - .Net core 5

# Application type - Console application

# Packages used:-

Configuration Nuget package

xUnit for Unit test

# Project Structure:- 

1) Created Separate class for Promotion and Strategies of promotion
2) The project is having a business layer that will interact with Presentation layer and Data
3) The required data is being populated from the json file using configuration manager
4) The project is using xUnit to run the test cases
5) Run the solution and enter the values in the console window.


# Test Cases covered
![image](https://user-images.githubusercontent.com/17158147/129192658-46415519-7470-4ec9-91bf-79c93a4eb07e.png)


 
