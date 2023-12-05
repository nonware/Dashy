import { Box, useTheme, useMediaQuery } from "@mui/material";
type Props = {};

const gridTemplateLargeScreens = `
    "a b c"
    "a b c"
    "a b c"
    "a b f"
    "d e f"
    "d e f"
    "d h i"
    "g h i"
    "g h j"
    "g h j"
`;

const gridTemplateSmallScreens = `
    "a"
    "a"
    "a"
    "a"
    "b"
    "b"
    "b"
    "c"
    "c"
    "c"
    "d"
    "d"
    "e"
    "e"
    "f"
    "f"
    "f"
    "g"
    "g"
    "g"
    "h"
    "h"
    "h"
    "h"
    "h"
    "i"
    "i"
    "j"
    "j"
`;

const Dashboard = (props: Props) => {
  const isAboveMediumScreens = useMediaQuery("(min-width: 1200px)");
  const { palette } = useTheme();
  return (
    <Box
      width="100%"
      height="100%"
      display="grid"
      gap="1.5rem"
      sx={
        isAboveMediumScreens
          ? {
              gridTemplateColumns: "repeat(3, minmax(370px, 1fr))",
              gridTemplateRows: "repeat(10, mimmax(60px, 1fr))",
              gridTemplateAreas: gridTemplateLargeScreens,
            }
          : {
              gridAutoColumns: "1fr",
              gridAutoRows: "80px",
              gridTemplateAreas: gridTemplateSmallScreens,
            }
      }
    >
      <Box bgcolor={palette.tertiary[500]} gridArea="a">
        1x4
      </Box>
      <Box bgcolor={palette.tertiary[500]} gridArea="b">
        1x4
      </Box>
      <Box bgcolor={palette.tertiary[500]} gridArea="c">
        1x3
      </Box>
      <Box bgcolor={palette.tertiary[500]} gridArea="d"></Box>
      <Box bgcolor={palette.tertiary[500]} gridArea="e"></Box>
      <Box bgcolor={palette.tertiary[500]} gridArea="f"></Box>
      <Box bgcolor={palette.tertiary[500]} gridArea="g"></Box>
      <Box bgcolor={palette.tertiary[500]} gridArea="h"></Box>
      <Box bgcolor={palette.tertiary[500]} gridArea="i"></Box>
      <Box bgcolor={palette.tertiary[500]} gridArea="j"></Box>
    </Box>
  );
};

export default Dashboard;
