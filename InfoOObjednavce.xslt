<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
  <xsl:output method="xml" indent="yes"/>

  <xsl:template match="/">
    <p>Byla vytvořena nová objednávka</p>
    <p>Číslo objednávky:</p>
    <table style="width:100%; border-collapse:collapse;">
      <tr>
        <td style="font-weight:bold">předmět:</td>
        <td style="font-weight:bold">množství:</td>
        <td style="font-weight:bold">cena:</td>
      </tr>
      <xsl:for-each select="/ShoppingCart/CartItems">
        <tr>
          <td>Title</td>
          <td></td>
          <td></td>
        </tr>
      </xsl:for-each>
      <tr>
        <td style="font-weight:bold" colspan="2">cena celkem:</td>
        <td style="font-weight:bold">
          <xsl:value-of select="/ShoppingCart/Total"/>
      </td>
      </tr>
    </table>
  </xsl:template>
</xsl:stylesheet>
