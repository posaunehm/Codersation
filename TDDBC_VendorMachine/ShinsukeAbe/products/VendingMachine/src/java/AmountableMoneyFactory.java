/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

/**
 *
 * @author mao
 */
class AmountableMoneyFactory {
    public static AmountableMoney createNewMoney(Integer value) {
        return new AmountableMoney(value);
    }
}
