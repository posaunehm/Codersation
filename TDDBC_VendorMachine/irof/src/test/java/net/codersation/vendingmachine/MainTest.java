package net.codersation.vendingmachine;

import static org.hamcrest.CoreMatchers.*;
import static org.junit.Assert.*;

import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.InputStream;
import java.io.PrintStream;

import org.junit.Test;

public class MainTest {

	@Test
	public void testName() throws Exception {
		ByteArrayOutputStream baos = new ByteArrayOutputStream();
		System.setOut(new PrintStream(baos));

		byte[] input = "hoge".getBytes();
		System.setIn(new ByteArrayInputStream(input));

		Main.main();

		String actual = baos.toString("UTF-8");
		assertThat(actual, is("hoge"));
	}
}
