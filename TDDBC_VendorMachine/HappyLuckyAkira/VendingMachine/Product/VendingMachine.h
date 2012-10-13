/*
 * VendingMachine.h
 *
 *  Created on: 2012/08/25
 *      Author: datsuns
 */

#ifndef VENDINGMACHINE_H_
#define VENDINGMACHINE_H_

#include <string>

const int JUICE_PRICE = 120;


class VendingMachine {
public:
	VendingMachine();
	virtual ~VendingMachine();
	int GetAmount();
	int InsertMoney( int money );
	int PayBack();
	std::string GetJuiceName(void);
	int GetJuicePrice(void);
	int GetJuiceStock(void);
	int GetSaleAmount(void);
	bool CanPurchase(void);

private:
	bool isValidMoney(int money);
	int amount_;
};

#endif /* VENDINGMACHINE_H_ */
