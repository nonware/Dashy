import { Box, useTheme, useMediaQuery } from "@mui/material";
import DashboardTile from "./DashboardTile";
type Props = {};

const gridTemplateLargeScreens = `
    "a b c"
    "a b c"
    "a b c"
    "d e f"
    "d e f"
    "d e f"
    "g h i"
    "g h i"
    "g h i"
`;

const gridTemplateSmallScreens = `
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
    "d"
    "e"
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
    "i"
    "i"
    "i"
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
      <DashboardTile gridArea="a"></DashboardTile>
      <DashboardTile gridArea="b"></DashboardTile>
      <DashboardTile gridArea="c"></DashboardTile>
      <DashboardTile gridArea="d"></DashboardTile>
      <DashboardTile gridArea="e"></DashboardTile>
      <DashboardTile gridArea="f"></DashboardTile>
      <DashboardTile gridArea="g"></DashboardTile>
      <DashboardTile gridArea="h"></DashboardTile>
      <DashboardTile gridArea="i"></DashboardTile>
    </Box>
  );
};

export default Dashboard;
