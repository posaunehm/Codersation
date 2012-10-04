/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.codersation.abe.vendingmachine;

import com.codersation.abe.vendingmachine.money.AcceptableMoney;
import com.codersation.abe.vendingmachine.money.AcceptableMoneyFactory;
import java.util.Map;
import org.junit.After;
import org.junit.AfterClass;
import org.junit.Before;
import org.junit.BeforeClass;
import org.junit.Test;
import static org.junit.Assert.*;
import static org.hamcrest.core.Is.*;

/**
 *
 * @author mao
 */
public class ChangeStockTest {
    
    private ChangeStock changeStock;
    
    public ChangeStockTest() {
    }
    
    @BeforeClass
    public static void setUpClass() {
    }
    
    @AfterClass
    public static void tearDownClass() {
    }
    
    @Before
    public void setUp() {
        changeStock = ChangeStockFactory.createNewChangeStock();
    }
    
    @After
    public void tearDown() {
    }
    
    // TODO 釣り銭ストックが初期状態で釣り銭に211円を要求すると例外がスローされる
    // TODO 釣り銭ストックが初期状態で釣り銭に16700円を要求すると例外がスローされる
    // TODO 初期状態の釣り銭ストックに50円1枚と10円3枚で払い出しを要求すると指定した金種の枚数が減る
    // TODO 釣り銭ストックに100円2枚の投入金額を追加した場合金種の指定した枚数が増える
    @Test
    public void 釣り銭ストックの初期状態では各金種が10枚ずつ存在する() {
        assertThat(changeStock.get(AcceptableMoneyFactory.createNewMoney(10)), is(10));
        assertThat(changeStock.get(AcceptableMoneyFactory.createNewMoney(50)), is(10));
        assertThat(changeStock.get(AcceptableMoneyFactory.createNewMoney(100)), is(10));
        assertThat(changeStock.get(AcceptableMoneyFactory.createNewMoney(500)), is(10));
        assertThat(changeStock.get(AcceptableMoneyFactory.createNewMoney(1000)), is(10));
    }
    
    @Test
    public void 釣り銭ストックが初期状態で釣り銭に80円を要求すると50円1枚と10円が3枚で計算される() {
        Map<AcceptableMoney, Integer> calculatedChanges = changeStock.calculateCount(80);
        assertThat(calculatedChanges.get(AcceptableMoneyFactory.createNewMoney(50)), is(1));
        assertThat(calculatedChanges.get(AcceptableMoneyFactory.createNewMoney(10)), is(3));
    }
    
    @Test
    public void 釣り銭ストックが初期状態で釣り銭に420円を要求すると100円4枚と10円2枚で計算される() {
        Map<AcceptableMoney, Integer> calculatedChanges = changeStock.calculateCount(420);
        assertThat(calculatedChanges.get(AcceptableMoneyFactory.createNewMoney(100)), is(4));
        assertThat(calculatedChanges.get(AcceptableMoneyFactory.createNewMoney(10)), is(2));
    }
}
