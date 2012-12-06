package net.codersation.vendingmachine;

import static org.hamcrest.CoreMatchers.*;
import static org.junit.Assert.*;

import java.io.BufferedReader;
import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.InputStream;
import java.io.PrintStream;
import java.io.StringReader;

import org.junit.Test;

public class MainTest {

	@Test
	public void 入力した金額の合計を出力する() throws Exception {
		ByteArrayOutputStream baos = new ByteArrayOutputStream();
		System.setOut(new PrintStream(baos));

		byte[] input = "100\n10\n".getBytes();
		System.setIn(new ByteArrayInputStream(input));

		Main.main();

		BufferedReader reader = new BufferedReader(new StringReader(baos.toString("UTF-8")));
		assertThat(reader.readLine(), is("Insert?> "));
		assertThat(reader.readLine(), is("credit: 100"));
		assertThat(reader.readLine(), is("Insert?> "));
		assertThat(reader.readLine(), is("credit: 110"));
	}
}
