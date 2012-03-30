<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
    <xsl:output method="html" indent="yes"/>

    <xsl:template match="/">
      <html>
        <head>
          <title>Edelaraudtee sõiduplaan</title>
        </head>
        <body>
          <p style="color:green">
            Siit vali jaam
          </p>
          <select>           
            <xsl:for-each select="//Peatus">
              <option value="{Nimetus}">
                <xsl:value-of disable-output-escaping="yes" select="Nimetus"/>
              </option>
            </xsl:for-each>
          </select>
          <br/>
          <select>
            <xsl:for-each select="//Peatus">
              <option value="{Nimetus}">
                <xsl:value-of disable-output-escaping="yes" select="Nimetus"/>
              </option>
            </xsl:for-each>
          </select>
        </body>
      </html>
    </xsl:template>
</xsl:stylesheet>
