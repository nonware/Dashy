import React from "react";
import DashboardTile from "@/components/DashboardTile";

type Props = {};

const Row1 = (props: Props) => {
  return (
    <>
      <DashboardTile gridArea="a"></DashboardTile>
      <DashboardTile gridArea="b"></DashboardTile>
      <DashboardTile gridArea="c"></DashboardTile>
    </>
  );
};

export default Row1;
