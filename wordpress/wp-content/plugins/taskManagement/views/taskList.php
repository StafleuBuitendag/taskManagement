<?php
    if ( !defined( 'ABSPATH' ) ) exit;
?>

<div>
    <?php // start foreach task element
        foreach ($tasks as $this_task) {
    ?>
    <div>
        <div>Name: <?php echo $this_task->name; ?> </div>
        <div>Status: <?php echo $this_task->status; ?> </div>
        
    </div>
    <?php // end foreach task element
        }
    ?>
</div>