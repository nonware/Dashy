import DashboardTile from "@/components/DashboardTile";
import React from "react";

type Props = {};

const Row3 = (props: Props) => {
  return (
    <>
      <DashboardTile gridArea="g"></DashboardTile>
      <DashboardTile gridArea="h"></DashboardTile>
      <DashboardTile gridArea="i"></DashboardTile>
    </>
  );
};

export default Row3;
